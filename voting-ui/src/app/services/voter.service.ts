import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Create, Voter } from '../models';

@Injectable({
  providedIn: 'root'
})
export class VoterService {
  private apiUrl = 'https://localhost:44336/api/Voters';

  constructor(private http: HttpClient) { }

  getVoters(): Observable<any> {
    return this.http.get<Voter[]>(this.apiUrl);
  }

  createVoter(req: Create): Observable<any> {
    return this.http.post(this.apiUrl, req);
  }
}
