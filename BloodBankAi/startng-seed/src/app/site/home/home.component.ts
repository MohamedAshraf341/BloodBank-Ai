import { Component, OnInit } from '@angular/core';
import { NewsService } from 'src/app/services/news.service';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  UpcomingNews:any =[];

  constructor(private NewsService:NewsService ){ 
   
    NewsService.getNews().subscribe((response)=> {
      this.UpcomingNews =response.articles ;
    },
    (err) =>{console.log(err)} )
    
  }
  ngOnInit(): void {
  }
  ngAfterViewInit(){
    document.getElementById('preloader').classList.add('hide');                 
}
}
