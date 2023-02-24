import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/internal/operators/catchError';
import { finalize } from 'rxjs/internal/operators/finalize';
import Phone from 'src/app/models/phone';
import { PhoneService } from 'src/app/service/phone.service';

@Component({
  selector: 'app-phone-details',
  templateUrl: './phone-details.component.html',
  styleUrls: ['./phone-details.component.scss']
})
export class PhoneDetailsComponent implements OnInit {

  phone!: Phone;
  stockMsg!: string;
  isLoading = true;
  currentId?: number;

  constructor(private router: Router, private phoneService: PhoneService) {
   }

  backButton() {
    this.router.navigate(['phones']);
  }

  ngOnInit(): void {
    this.phone = this.router.getCurrentNavigation()?.extras?.state?.['Phone'];

      // if no phone object is present in NavigationExtras, attempt GetById
      if (!this.phone) {
        let url = this.router.url;

        let parts = url.split("/");
        let result = parts[parts.length - 1];

        this.currentId = parseInt(result);

        if(this.currentId) {
          this.subscribeForPhone(this.currentId);
        }
        else {
          //console.log('cannot extract id');
          this.router.navigate(['**'], { skipLocationChange: true });
        }
      } else {
        this.setStockMsg();
        this.isLoading = false;    
      }
  }

  subscribeForPhone(id: number): void {
    this.phoneService.getById(id)
    .pipe(
      catchError(error => {
        throw error;
      }),
      finalize(() => {
        if(!this.phone) { 
          //console.log('cannot call api for phone');
          this.router.navigate(['**'], { skipLocationChange: true }); 
        } else {
          this.setStockMsg();
          this.isLoading = false;
        }

      })
    )    
    .subscribe(p => this.phone = p);
  }  

  setStockMsg(){
    let stock: number = 0;
    if (!this.phone.stock || this.phone.stock < 0) stock = this.phone.stock;

    switch(true) { 
      case (stock == 0): { 
         this.stockMsg = 'Out of stock.'
         break; 
      } 
      case (stock <= 5): { 
        this.stockMsg = `Only ${stock} left!`
         break; 
      } 
      default: { 
         this.stockMsg = stock + ' in stock.';
         break; 
      } 
   } 
  }
}
