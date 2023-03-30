import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app.routing';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};
import { FormsModule } from '@angular/forms';

import { HttpClient, HttpClientModule } from '@angular/common/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader'; 
export function HttpLoaderFactory(httpClient: HttpClient) { 
  return new TranslateHttpLoader(httpClient, './assets/i18n/', '.json');
}

import { AppSettings } from './app.settings';
import { AppComponent } from './app.component';
import { PagesComponent } from './pages/pages.component';
import { HeaderComponent } from './theme/components/header/header.component';
import { FooterComponent } from './theme/components/footer/footer.component';
import { SidebarComponent } from './theme/components/sidebar/sidebar.component';
import { VerticalMenuComponent } from './theme/components/menu/vertical-menu/vertical-menu.component';
import { HorizontalMenuComponent } from './theme/components/menu/horizontal-menu/horizontal-menu.component';
import { BreadcrumbComponent } from './theme/components/breadcrumb/breadcrumb.component';
import { BackTopComponent } from './theme/components/back-top/back-top.component';
import { UserMenuComponent } from './theme/components/user-menu/user-menu.component';
import { BlankComponent } from './pages/blank/blank.component';
import { SearchComponent } from './pages/search/search.component';
import { NotFoundComponent } from './pages/errors/not-found/not-found.component';
import { FlagsMenuComponent } from './theme/components/flags-menu/flags-menu.component';
import { NavBarComponent } from './site/fixed/nav-bar/nav-bar.component';
import { HomeComponent } from './site/home/home.component';
import { SecondNavComponent } from './site/fixed/second-nav/second-nav.component';
import { FooterMenuComponent } from './site/fixed/footer-menu/footer-menu.component';
import { AboutUsComponent } from './site/about-us/about-us.component';
import { ContactComponent } from './site/contact/contact.component';
import { DonationProcessComponent } from './site/donation-process/donation-process.component';
import { DonationProcessDesComponent } from './site/donation-process-des/donation-process-des.component';
import { WhatHappenDonatebloodComponent } from './site/what-happen-donateblood/what-happen-donateblood.component';
import { FristTimeDonationComponent } from './site/frist-time-donation/frist-time-donation.component';
import { SelectTimeTodonationComponent } from './site/select-time-todonation/select-time-todonation.component';

@NgModule({  
  imports: [
    BrowserModule,
    PerfectScrollbarModule,
    AppRoutingModule,
    HttpClientModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    FormsModule
  ],
  declarations: [
    AppComponent,
    PagesComponent,
    HeaderComponent,
    FooterComponent,
    SidebarComponent,
    VerticalMenuComponent,
    HorizontalMenuComponent,
    BreadcrumbComponent,
    BackTopComponent,
    UserMenuComponent,
    BlankComponent,
    SearchComponent,    
    NotFoundComponent,
    FlagsMenuComponent,
    NavBarComponent,
    SecondNavComponent,
    FooterMenuComponent,
    HomeComponent,
    AboutUsComponent,
    ContactComponent,
    DonationProcessComponent,
    DonationProcessDesComponent,
    WhatHappenDonatebloodComponent,
    FristTimeDonationComponent,
    SelectTimeTodonationComponent
  ],
  providers: [ 
    AppSettings,
    { provide: PERFECT_SCROLLBAR_CONFIG, useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG }
  ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
