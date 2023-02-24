import { Component, Input, OnInit } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import Phone from 'src/app/models/phone';

@Component({
  selector: 'app-phone-info',
  templateUrl: './phone-info.component.html',
  styleUrls: ['./phone-info.component.scss']
})
export class PhoneInfoComponent implements OnInit {

  @Input() phone!: Phone;
  
  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  handleClick() {
      const navigationExtras: NavigationExtras = {state: {Phone: this.phone}};
      this.router.navigate( ['details/' + this.phone.id], navigationExtras );
  }

}
