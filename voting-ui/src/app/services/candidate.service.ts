import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Candidate, Create } from '../models';

@Injectable({
  providedIn: 'root'
})
export class CandidateService {
  private apiUrl = 'https://localhost:44336/api/Candidates';

  constructor(private http: HttpClient) { }

  getCandidates(): Observable<any> {
    return this.http.get<Candidate[]>(this.apiUrl);
  }

  createCandidates(req: Create): Observable<any> {
    return this.http.post(this.apiUrl, req);
  }
}