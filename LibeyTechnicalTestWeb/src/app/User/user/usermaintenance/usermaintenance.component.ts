import swal from 'sweetalert2';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UtilsService } from 'src/app/core/service/utils/utils.service.ts.service';
import { DocumentType } from 'src/app/entities/documentType';
import { Region } from 'src/app/entities/region';
import { Province } from 'src/app/entities/province';
import { Ubigeo } from 'src/app/entities/ubigeo';
import { forkJoin } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { LibeyUserService } from 'src/app/core/service/libeyuser/libeyuser.service';
import { LibeyUser } from 'src/app/entities/libeyuser';
@Component({
  selector: 'app-usermaintenance',
  templateUrl: './usermaintenance.component.html',
  styleUrls: ['./usermaintenance.component.css']
})
export class UsermaintenanceComponent implements OnInit {
  form!: FormGroup;
  comboTipoDocumento !: DocumentType[];
  comboRegion !: Region[];
  comboProvincia !: Province[];
  comboUbigeo !: Ubigeo[];
  selectedRegion !: string;
  documentNumberSelected : string = "";
  libeyUserEdit : LibeyUser = {};
  constructor(private utilsService: UtilsService, 
              private libeyUserService: LibeyUserService,
              private route: ActivatedRoute,
              private router: Router) { }
  ngOnInit(): void {
    this.initForm();
    this.cargarCombos();
    this.route.queryParamMap.subscribe(params => {
      if (params.get('documentNumber')){
        this.documentNumberSelected = params.get('documentNumber') ?? '';
        this.libeyUserService.Find(this.documentNumberSelected).subscribe(res => {
          if (res){
            this.libeyUserEdit = res;
            if(res.ubigeoCode){
              this.libeyUserEdit.regionCode = res.ubigeoCode.substring(0,2);
              this.changeProvince(this.libeyUserEdit.regionCode);
              this.libeyUserEdit.provinceCode = res.ubigeoCode.substring(0,4);
              this.changeUbigeo(this.libeyUserEdit.regionCode,this.libeyUserEdit.provinceCode);
              this.libeyUserEdit.ubigeoCode = res.ubigeoCode
              this.form.patchValue(this.libeyUserEdit);
            }
          }
          
        });
      }
    });
    
  }

  private async initForm(){
    this.form = new FormGroup({
      documentTypeId: new FormControl(this.libeyUserEdit.documentTypeId, Validators.required),
      documentNumber: new FormControl(this.libeyUserEdit.documentNumber, Validators.required),
      name: new FormControl(this.libeyUserEdit.name),
      fathersLastName: new FormControl(this.libeyUserEdit.fathersLastName),
      mothersLastName: new FormControl(this.libeyUserEdit.mothersLastName),
      address: new FormControl(this.libeyUserEdit.address),
      regionCode: new FormControl(this.libeyUserEdit.regionCode, Validators.required),
      provinceCode: new FormControl(this.libeyUserEdit.provinceCode, Validators.required),
      ubigeoCode: new FormControl(this.libeyUserEdit.ubigeoCode, Validators.required),
      phone: new FormControl(this.libeyUserEdit.phone),
      email: new FormControl(this.libeyUserEdit.email),
      password: new FormControl(this.libeyUserEdit.password),
    });
  }

  async cargarCombos(){

    await forkJoin({
      documentTypes: this.utilsService.GetAllDocumentType(),
      regions: this.utilsService.GetAllRegion()
    }).subscribe({
      next: ({ documentTypes, regions }) => {
        this.comboTipoDocumento = documentTypes;
        this.comboRegion = regions;
      }
    });

  }

  changeProvince(regionCode: string){
    this.selectedRegion = regionCode;
    this.utilsService.GetProvince(regionCode).subscribe( res => {
      this.comboProvincia = res;
    });
  }

  changeUbigeo(regionCode: string,provinceCode: string){
    this.utilsService.GetUbigeo(regionCode,provinceCode).subscribe( res => {
      this.comboUbigeo = res;
    });
  }

  limpiar(){
    this.form.reset();
    this.comboProvincia = [];
    this.comboUbigeo = [];
  }

  Submit(){
    //swal.fire("Oops!", "Something went wrong!", "error");
    this.save();
  }

  back(){
    this.router.navigate(['user/card']);
  }

  save(){
    this.libeyUserService.save(this.form.value).subscribe(res => {
      this.router.navigate(['user/list']);
      swal.fire("Exito!", "El registro fue guardado.", "success");
    });
  }

}