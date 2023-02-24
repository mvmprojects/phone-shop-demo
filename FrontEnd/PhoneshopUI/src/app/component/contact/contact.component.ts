import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {

  formMain!: FormGroup;

  labelName = 'Name';
  labelEmail = 'Email';
  labelMessage = 'Message'; 
  requirementWarning = 'This field is required.';
  emailWarning = 'Please enter a valid email address.';

  constructor(private formBuilder: FormBuilder){ 
    this.formMain = this.formBuilder.group({
      contactName: new FormControl('', [Validators.required]),
      contactEmail: new FormControl('', [Validators.required, Validators.email]),  
      contactMessage: new FormControl('', [Validators.required])
    });
  }

  ngOnInit(): void {
  }

  onSubmit(): void {
    console.log(this.formMain.value);
  }

}
