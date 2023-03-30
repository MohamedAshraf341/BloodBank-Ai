import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-donation-process-des',
  templateUrl: './donation-process-des.component.html',
  styleUrls: ['./donation-process-des.component.scss']
})
export class DonationProcessDesComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  stylee={
    color:"red",    
    marginTop:"10px",
  }
  ngAfterViewInit(){
    document.getElementById('preloader').classList.add('hide');                 
}
}
