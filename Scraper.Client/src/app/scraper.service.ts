import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../environments/environment.development';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';
import { Observable } from 'rxjs';
import { ScrapedResults } from './models/apiresponse';


@Injectable({
  providedIn: 'root'
})
export class ScraperService {

  constructor() { }

  private http = inject(HttpClient);
  private apiUrl = environment.apiUrl;
  position : number[] = [];

    insertSearchResults(formData: { SearchKeyword: string; SearchUrl: string; SearchEngine: string; MaxCount: number }): Observable<any> {
      return this.http.post<any>(`${this.apiUrl}/InsertSearchResults`, formData);
    }
  
    fetchSearchResults(): Observable<ScrapedResults[]> {
      return this.http.get<ScrapedResults[]>(`${this.apiUrl}/SearchResults`);
    }

    getPositions():Observable<any>{
      return this.http.get<any>(`${this.apiUrl}/Position`);
    }
  
  }

