import { Injectable } from '@angular/core';
import { CastvoteService } from './castvote.service';
import { CandidateService } from './candidate.service';
import { VoterService } from './voter.service';
import { forkJoin, Observable } from 'rxjs';
import { Candidate, CastVote, Create, Voter } from '../models';

@Injectable({
  providedIn: 'root'
})
export class FacadeService {

  constructor(
    private candidateService: CandidateService,
    private castvoteService: CastvoteService,
    private voterService: VoterService
  ) { }

  getAllData(): Observable<{ voters: Voter[], candidates: Candidate[] }> {
    return forkJoin({
      voters: this.voterService.getVoters(),
      candidates: this.candidateService.getCandidates()
    });
  }

  create(req: Create, isVoter: boolean): Observable<any> {
    if (isVoter) {
      return this.voterService.createVoter(req);
    } 
    
    return this.candidateService.createCandidates(req);
  }

  castVote(req: CastVote): Observable<any> {
    return this.castvoteService.castVote(req);
  }
}
