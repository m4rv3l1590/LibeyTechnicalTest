import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LibeyUserService } from 'src/app/core/service/libeyuser/libeyuser.service';
import { LibeyUser } from 'src/app/entities/libeyuser';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-userlist',
  templateUrl: './userlist.component.html',
  styleUrls: ['./userlist.component.css']
})
export class UserlistComponent implements OnInit {

  libeyUserList!: LibeyUser[];

  constructor(private libeyUserService: LibeyUserService,
              private router: Router) { }

  ngOnInit(): void {
    this.get();
  }

  get(){
    this.libeyUserService.FindAll().subscribe(res => {
      this.libeyUserList= res;
    });
  }

  back(){
    this.router.navigate(['user/card']);
  }

  edit(documentNumber: string){
    this.router.navigate(['user/maintenance'], { queryParams: { documentNumber: documentNumber } });
  }

  delete(documentNumber: string){
    Swal.fire({
      title: '¿Estás seguro?',
      text: 'Esta acción eliminará el registro.',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        this.libeyUserService.Delete(documentNumber).subscribe(res => {
          Swal.fire('¡Éxito!', 'El registro fue eliminado.', 'success');
          this.get();
        });
      }
    });
  }

}
