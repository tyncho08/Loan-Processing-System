import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenService {

  private tokenApi = `${environment.apiUrl.replace('/api', '')}/token`;

  constructor(private http: HttpClient) { }

  // method of requesting to the backend for a token
  tokenGen(userData): Observable<any> {
    var headersForTokenApi = new HttpHeaders({
      'Content-Type': 'application/x-www-form-urlencoded'
    });
    var data = "grant_type=password&username=" + userData.useremail + "&password=" + userData.userpass;
    return this.http.post(this.tokenApi, data, { headers: headersForTokenApi });
  }
}
