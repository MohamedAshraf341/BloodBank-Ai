import { Component } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { banks } from 'src/app/Models/Admin';
import { AcountService } from 'src/app/Services/acount.service';
import { ModeratorService } from 'src/app/Services/moderator.service';

@Component({
  selector: 'app-moderator-page',
  templateUrl: './moderator-page.component.html',
  styleUrls: ['./moderator-page.component.scss']
})
export class ModeratorPageComponent {
  user:any;
  constructor(private moderatorService:ModeratorService,private acountService:AcountService,
    private sanitizer: DomSanitizer,){}
  ngOnInit(): void {
    this.getAllBanks();
  }  
  public searchText: string;
  public p:any;
  banks:banks[]=[];
  getAllBanks()
  {
    this.user=this.acountService.getUser();
    if(this.user.id != null)
    {
      this.moderatorService.getAllBanks(this.user.id).subscribe((res)=>{
        if(res.success)
        {
          this.banks=res.data;
        }
      })
    }

  }
  getPictureUrl(picture: string): any {
    return this.sanitizer.bypassSecurityTrustUrl(`data:image/png;base64,${picture}`);
  }
}
