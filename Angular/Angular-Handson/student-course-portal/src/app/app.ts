import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { LoadingService } from './services/loading';

@Component({
  selector: 'app-root',
  standalone: false,
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  // Hands-On 8: Global loading spinner bound to interceptor's BehaviorSubject
  isLoading$: Observable<boolean>;

  constructor(private loadingService: LoadingService) {
    this.isLoading$ = this.loadingService.isLoading$;
  }
}
