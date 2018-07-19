import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HttpParams } from '@angular/common/http';
//import { Observable } from 'rxjs/Observable';
import { QuestionService } from './question.service';
import { Question } from './question';
import { Answer } from './answers';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css']
})
export class QuestionComponent implements OnInit {
  answer: Answer;
  question: Question;
  curatedExperienceId: string;

  getQuestion(): void {
    this.curatedExperienceId = "f3daa1e4-5f20-47ce-ab6d-f59829f936fe";
    let params = new HttpParams()
      .set("curatedExperienceId", this.curatedExperienceId);
    this.questionService.getQuestion(params)
      .subscribe(question => this.question = { ...question });
  }

  onSubmit(gaForm: NgForm): void {
    const params = {
      "curatedExperienceId": "f3daa1e4-5f20-47ce-ab6d-f59829f936fe",
      "answersDocId": "c3469dee-e628-4377-b4eb-81923766b02f",
      "buttonId": "2b92e07b-a555-48e8-ad7b-90b99ebc5c96",
      "multiSelectionFieldIds": [],
      "fields": [{
        "fieldId": "",
        "value": "",
      }]
    }
    this.questionService.getNextQuestion(params)
        .subscribe(response => this.question = { ...response });
  };

  constructor(private questionService: QuestionService) { }

  ngOnInit() {
    this.getQuestion();
  }
}