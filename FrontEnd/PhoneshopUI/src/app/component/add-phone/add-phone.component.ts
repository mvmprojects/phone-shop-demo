import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-phone',
  templateUrl: './add-phone.component.html',
  styleUrls: ['./add-phone.component.scss']
})
export class AddPhoneComponent implements OnInit {

  formMain!: FormGroup;
  labelBrand = 'Brand';
  labelType = 'Type';
  labelDescription = 'Description'; 
  labelPrice = 'Price';
  labelStock = 'Stock';
  decimalRegex = '^\-?[0-9]+(?:\.[0-9]{1,2})?$';
  positiveIntRegex = '^[0-9]+$';
  requirementWarning = 'This field is required.';
  valueWarning = 'Please enter a valid value.';

  constructor(private formBuilder: FormBuilder){
    this.formMain = this.formBuilder.group({
      phoneBrand: new FormControl('', [Validators.required]),
      phoneType: new FormControl('', [Validators.required]),
      phoneStock: new FormControl('', 
      [
        Validators.required, 
        Validators.pattern(this.positiveIntRegex)
      ]),
      phonePrice: new FormControl('', 
      [
        Validators.required, 
        Validators.min(0), 
        Validators.pattern(this.decimalRegex)
      ]),
      phoneDescription: new FormControl(''),      
    });
  }

  ngOnInit(): void {
  }

  submitForm(): void {
    
  }

}
