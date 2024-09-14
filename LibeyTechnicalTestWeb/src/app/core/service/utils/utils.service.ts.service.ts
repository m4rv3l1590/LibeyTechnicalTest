import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from "../../../../environments/environment";
import { Observable } from 'rxjs';
import { DocumentType } from 'src/app/entities/documentType';
import { Region } from 'src/app/entities/region';
import { Province } from 'src/app/entities/province';
import { Ubigeo } from 'src/app/entities/ubigeo';

@Injectable({
  providedIn: 'root'
})
export class UtilsService {

  constructor(private http: HttpClient) {}

  GetAllDocumentType(): Observable<DocumentType[]> {
		const uri = `${environment.pathLibeyTechnicalTest}Utils/GetAllDocumentType`;
		return this.http.get<DocumentType[]>(uri);
	}

  GetAllRegion(): Observable<Region[]> {
		const uri = `${environment.pathLibeyTechnicalTest}Utils/GetAllRegion`;
		return this.http.get<Region[]>(uri);
	}

  GetProvince(regionCode: string): Observable<Province[]> {
		const uri = `${environment.pathLibeyTechnicalTest}Utils/GetProvince?regionCode=${regionCode}`;
		return this.http.get<Province[]>(uri);
	}

  GetUbigeo(regionCode: string, provinceCode: string): Observable<Ubigeo[]> {
		const uri = `${environment.pathLibeyTechnicalTest}Utils/GetUbigeo?regionCode=${regionCode}&provinceCode=${provinceCode}`;
		return this.http.get<Ubigeo[]>(uri);
	}

}
