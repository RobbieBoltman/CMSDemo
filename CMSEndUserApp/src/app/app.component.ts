import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import {ReactiveFormsModule} from '@angular/forms';

@Component({
    selector: 'app-root',
    imports: [CommonModule, RouterOutlet, NgbModule, RouterModule, ReactiveFormsModule],
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'CMS Demo';
  constructor(private router: Router){   
  }
  logout() {
    // Perform logout actions (e.g., clear session, remove tokens, etc.)
    console.log('User logged out');

    // Redirect to the login page
    this.router.navigate(['/login']);
  }
}
