import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CastVote } from '../models';

@Injectable({
  providedIn: 'root'
})
export class CastvoteService {
  private apiUrl = 'https://localhost:44336/api/CastVote/votes';

  constructor(private http: HttpClient) { }

  castVote(req: CastVote): Observable<any> {
    return this.http.post(this.apiUrl, req);
  }
}
