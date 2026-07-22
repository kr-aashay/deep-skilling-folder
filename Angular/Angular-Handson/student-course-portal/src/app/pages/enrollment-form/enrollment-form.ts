import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-enrollment-form',
  standalone: false,
  templateUrl: './enrollment-form.html',
  styleUrl: './enrollment-form.css'
})
export class EnrollmentForm {
  submitted = false;
  studentName = '';
  studentEmail = '';
  courseId: number | null = null;
  preferredSemester = 'Odd';
  agreeToTerms = false;

  onSubmit(form: NgForm): void {
    console.log('Form value:', form.value);
    console.log('Form valid:', form.valid);
    if (form.valid) {
      this.submitted = true;
    }
  }

  onReset(form: NgForm): void {
    form.resetForm();
    this.submitted = false;
  }
}
