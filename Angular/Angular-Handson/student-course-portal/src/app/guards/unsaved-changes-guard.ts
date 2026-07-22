import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { ReactiveEnrollmentForm } from '../pages/reactive-enrollment-form/reactive-enrollment-form';

@Injectable({ providedIn: 'root' })
export class UnsavedChangesGuard implements CanDeactivate<ReactiveEnrollmentForm> {
  canDeactivate(component: ReactiveEnrollmentForm): boolean {
    // Check if form is dirty — has unsaved user input
    if (component.enrollForm?.dirty) {
      return window.confirm('You have unsaved changes. Leave anyway?');
    }
    return true;
  }
}
