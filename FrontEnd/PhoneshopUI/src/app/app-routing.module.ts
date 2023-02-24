import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddPhoneComponent } from './component/add-phone/add-phone.component';
import { ContactComponent } from './component/contact/contact.component';
import { HomeComponent } from './component/home/home.component';
import { PageNotFoundComponent } from './component/page-not-found/page-not-found.component';
import { PhoneDetailsComponent } from './component/phone-details/phone-details.component';
import { PhoneListComponent } from './component/phone-list/phone-list.component';

const routes: Routes = [
  { path: 'phones', component: PhoneListComponent },
  { path: 'details/:id', component: PhoneDetailsComponent },
  { path: 'addphone', component: AddPhoneComponent },
  { path: 'contact', component: ContactComponent },
  { path: '', component: HomeComponent}, 
  //{ path: 'home', component: HomeComponent},
  //{ path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
