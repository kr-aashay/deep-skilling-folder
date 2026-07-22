import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AuthService {
  // Hardcoded for Hands-On 7 — replace with real login logic later
  isLoggedIn = true;
}
