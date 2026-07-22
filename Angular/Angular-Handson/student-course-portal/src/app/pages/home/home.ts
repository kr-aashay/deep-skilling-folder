import { Component, OnInit, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home implements OnInit, OnDestroy {
  // Hands-On 2: portalName used for string interpolation
  portalName = 'Student Course Portal';

  // Hands-On 2: Property binding — controls button disabled state
  isPortalActive = true;

  // Hands-On 2: Message displayed after Enroll Now click
  message = '';

  // Hands-On 2: Two-way binding with ngModel
  searchTerm = '';

  // Hands-On 6: Will be replaced with live count from CourseService
  coursesAvailable = 12;

  ngOnInit(): void {
    // Lifecycle hook: fires once after inputs are set — ideal for data fetching
    console.log('HomeComponent initialised — courses loaded');
  }

  ngOnDestroy(): void {
    // Lifecycle hook: fires when component is removed from DOM
    // Use this to unsubscribe from Observables to prevent memory leaks
    console.log('HomeComponent destroyed');
  }

  onEnrollClick(): void {
    this.message = 'Enrollment opened!';
  }
}
