import { Component, OnInit } from '@angular/core';
import { catchError } from 'rxjs/internal/operators/catchError';
import { finalize } from 'rxjs/internal/operators/finalize';
import Phone from 'src/app/models/phone';
import { PhoneService } from 'src/app/service/phone.service';

@Component({
  selector: 'app-phone-list',
  templateUrl: './phone-list.component.html',
  styleUrls: ['./phone-list.component.scss']
})
export class PhoneListComponent implements OnInit {

  phones!: Phone[];
  isLoading = true; // combine with *ngIf and a spinner animation

  constructor(private phoneService: PhoneService) {    
    this.phoneService.getPhones();
  }

  ngOnInit(): void {
    this.subscribeForPhones()
  }

  subscribeForPhones(): void {
    this.phoneService.getPhones()
    .pipe(
      catchError(error => {
        throw error;
      }),
      finalize(() => {
        this.isLoading = false;
        if(!this.phones) this.defaultPhone();
      })
    )    
    .subscribe(p => this.phones = p);
  }

  defaultPhone(): void{
    this.phones = [
      {
        id: 0,
        brand: { id: 0, name: 'Default brand' },        
        type: 'Default type',
        price: 10,
        stock: 0,
        description: 'Default description.'
      }
    ];
  }
}
