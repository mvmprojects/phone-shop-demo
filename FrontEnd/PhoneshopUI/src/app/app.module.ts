import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PhoneListComponent } from './component/phone-list/phone-list.component';
import { PhoneInfoComponent } from './component/phone-info/phone-info.component';
import { AddPhoneComponent } from './component/add-phone/add-phone.component';
import { PhoneDetailsComponent } from './component/phone-details/phone-details.component';
import { AppMaterialModule } from './app-material/app-material.module';
import { HomeComponent } from './component/home/home.component';
import { PageNotFoundComponent } from './component/page-not-found/page-not-found.component';
import { ContactComponent } from './component/contact/contact.component';

@NgModule({
  declarations: [
    AppComponent,
    PhoneListComponent,
    PhoneInfoComponent,
    AddPhoneComponent,
    PhoneDetailsComponent,
    HomeComponent,
    PageNotFoundComponent,
    ContactComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppMaterialModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
