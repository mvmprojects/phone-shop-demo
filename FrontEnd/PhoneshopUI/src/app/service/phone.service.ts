import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import Phone from '../models/phone';
import { Observable } from 'rxjs/internal/Observable';
//import { catchError } from 'rxjs/internal/operators/catchError';
//import { of } from 'rxjs/internal/observable/of';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PhoneService {

  phones!: Phone[];
  phonesUrl: string = environment.apiUrl + 'Phones';

  constructor(public http: HttpClient) {
  }

  getPhones(): Observable<Phone[]> {
    //console.log(this.phonesUrl)
    // return this.http.get<Phone[]>(this.phonesUrl).pipe(
    //   catchError(this.handleError<Phone[]>(`was fetching phones`))
    // );
    return this.http.get<Phone[]>(this.phonesUrl);
  }

  getById(id: number): Observable<Phone> {
    return this.http.get<Phone>(this.phonesUrl + `/${id}`);
  }

  // private handleError<T>(operation = 'operation', result?: T) {
  //   return (error: any): Observable<T> => {
  //     console.error(error);
  //     // App continues to run by returning an empty result.
  //     return of(result);
  //   };
  // }    
}
