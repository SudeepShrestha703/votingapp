import { Component, model, TemplateRef, ViewChild } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FacadeService } from './services/facade.service';
import { Candidate, CastVote, Create, Voter } from './models';
import { MaterialModule } from './material/material.module';
import { MatDialog } from '@angular/material/dialog';
import { FormControl, FormGroup, FormsModule, NgForm, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  @ViewChild('dialogTemplate') dialogTemplate!: TemplateRef<any>;
  readonly nameFormControl = model('');

  title = 'voting-ui';
  voters: Voter[] = [];
  candidates: Candidate[] = [];
  voterColumns = ['name', 'hasVoted'];
  candidateColumns = ['name', 'votes'];
  dialogHeader: string = 'Voter';
  isVoter: boolean = false;
  castVotingFormGroup = new FormGroup({
    voterId: new FormControl<number>(0, [Validators.required]),
    candidateId: new FormControl<number>(0, [Validators.required]),
  });
  votersList: Voter[] = [];

  get disableFormButton(): boolean {
    const formValue = this.castVotingFormGroup.value;

    if (!formValue.candidateId || !formValue.voterId) {
      return false
    }

    return true;
  }
  
  constructor(
    public dialog: MatDialog,
    private facadeService: FacadeService) {}

  ngOnInit(): void {
    this.getData();
  }

  getData(): void {
    this.facadeService.getAllData().subscribe(data => {
      this.voters = data.voters;
      this.votersList = data.voters.filter(x => !x.hasVoted);
      this.candidates = data.candidates;
    });
  }

  castVote(): void {
    if (!this.castVotingFormGroup.valid) {
      return;
    }

    const formValue = this.castVotingFormGroup.value;

    const request: CastVote = {
      candidateId: formValue.candidateId || 0,
      voterId: formValue.voterId || 0
    };

    this.facadeService.castVote(request)
    .subscribe(() => {
      this.castVotingFormGroup.reset();
      this.getData();
    });
  }

  createData(form: NgForm): void {
    const request: Create = {
      name: this.nameFormControl()
    };

    const alreadyExist = this.voters
      .filter(x => x.name.trim() === this.nameFormControl().trim()).length ||
      this.candidates
        .filter(x => x.name.trim() === this.nameFormControl().trim()).length;

    if (alreadyExist) {
      const control = form.controls['nameFormControl'];
      control.setErrors({ ...control.errors, duplicate: 'Name already exist' });

      return
    }

    this.facadeService.create(request, this.isVoter)
    .subscribe(() => {
      this.dialog.closeAll();
      this.getData();
      this.nameFormControl.set('');
    })
  }

  addData(isVoter: boolean = false) {
    this.nameFormControl.set('');

    if (!isVoter) {
      this.dialogHeader = 'Candidate';
    } else {
      this.dialogHeader = 'Voter';
    }

    this.isVoter = isVoter;

    this.dialog.open(this.dialogTemplate);

  }
}
