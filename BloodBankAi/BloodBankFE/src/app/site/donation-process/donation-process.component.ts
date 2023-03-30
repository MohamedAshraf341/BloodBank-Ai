import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-donation-process',
  templateUrl: './donation-process.component.html',
  styleUrls: ['./donation-process.component.scss']
})
export class DonationProcessComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  ngAfterViewInit(){
    document.getElementById('preloader').classList.add('hide');                 
}
}
