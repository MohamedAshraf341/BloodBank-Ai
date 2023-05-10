import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import {TimeAgoPipe} from 'time-ago-pipe'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavBarComponent } from './Shared/nav-bar/nav-bar.component';
import { SecondNavComponent } from './Shared/second-nav/second-nav.component';
import { FooterMenuComponent } from './Shared/footer-menu/footer-menu.component';
import { AboutUsComponent } from './site/about-us/about-us.component';
import { ContactComponent } from './site/contact/contact.component';
import { DonationProcessComponent } from './site/donation-process/donation-process.component';
import { DonationProcessDesComponent } from './site/donation-process-des/donation-process-des.component';
import { FristTimeDonationComponent } from './site/frist-time-donation/frist-time-donation.component';
import { HomeComponent } from './site/home/home.component';
import { SelectTimeTodonationComponent } from './site/select-time-todonation/select-time-todonation.component';
import { WhatHappenDonatebloodComponent } from './site/what-happen-donateblood/what-happen-donateblood.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {NgxPaginationModule} from 'ngx-pagination';
import { FindDonorsComponent } from './site/find-donors/find-donors.component';
import { SearchPipe } from './Shared/pipes/search.pipe';
import { FindBanksComponent } from './site/find-banks/find-banks.component';
import { AdminPageComponent } from './site/Admin/admin-page/admin-page.component';
import { BankRegisterComponent } from './site/Admin/bank-register/bank-register.component';
import { BankEditComponent } from './site/Admin/bank-edit/bank-edit.component';
import { ToastrModule } from 'ngx-toastr';
import { AdministrationComponent } from './site/administration/administration.component';
import { LoginComponent } from './Auth/login/login.component';
import { SignUpComponent } from './Auth/sign-up/sign-up.component';
import { EditProfileComponent } from './Auth/edit-profile/edit-profile.component';
@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    SecondNavComponent,
    FooterMenuComponent,
    AboutUsComponent,
    ContactComponent,
    DonationProcessComponent,
    DonationProcessDesComponent,
    FristTimeDonationComponent,
    HomeComponent,
    SelectTimeTodonationComponent,
    WhatHappenDonatebloodComponent,
    FindDonorsComponent,
    SearchPipe,
    FindBanksComponent,
    AdminPageComponent,
    BankRegisterComponent,
    BankEditComponent,
    AdministrationComponent,
    LoginComponent,
    SignUpComponent,
    EditProfileComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    BrowserAnimationsModule, 
    CommonModule,
    NgxPaginationModule,
    ToastrModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
