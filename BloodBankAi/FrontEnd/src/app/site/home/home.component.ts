import { Component, OnInit } from '@angular/core';
import { newsModel } from 'src/app/Models/newModel'; 
import { HelperService } from 'src/app/Services/helper.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  public p:any;
  UpcomingNews:any[]=[];
  

  constructor(private NewsService:HelperService ){        
  }
  ngOnInit(): void {
    this.getNews();
    console.log(this.UpcomingNews);
  }
  getNews()
  {
    this.NewsService.getNews().subscribe((res)=>
    {
      this.UpcomingNews=res.articles;
      console.log(res);

    })
  }
}
