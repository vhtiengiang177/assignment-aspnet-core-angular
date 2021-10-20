import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  constructor(
      private router: Router,
      private route: ActivatedRoute,
      private authService : AuthService) { }

  ngOnInit() {
  }

  logout(){
    this.authService.logout();
    if(this.route.snapshot['_routerState'].url != '/')    
      this.router.navigate(['/']);
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle(){
    this.isExpanded = true;
  }
  
}
