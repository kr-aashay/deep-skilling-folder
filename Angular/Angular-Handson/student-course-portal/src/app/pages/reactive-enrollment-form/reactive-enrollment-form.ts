import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, FormControl, Validators, AbstractControl, ValidationErrors } from '@angular/forms';

// Custom synchronous validator — rejects course codes starting with 'XX'
export function noCourseCode(control: AbstractControl): ValidationErrors | null {
  if (control.value && String(control.value).startsWith('XX')) {
    return { noCourseCode: true };
  }
  return null;
}

// Custom async validator — simulates an API check for taken emails
export function simulateEmailCheck(control: AbstractControl): Promise<ValidationErrors | null> {
  return new Promise(resolve => {
    setTimeout(() => {
      resolve(control.value?.includes('test@') ? { emailTaken: true } : null);
    }, 800);
  });
}

@Component({
  selector: 'app-reactive-enrollment-form',
  standalone: false,
  templateUrl: './reactive-enrollment-form.html',
  styleUrl: './reactive-enrollment-form.css'
})
export class ReactiveEnrollmentForm implements OnInit {
  enrollForm!: FormGroup;

  // Getter — better than casting in template; keeps template clean and type-safe
  // Avoids (enrollForm.get('additionalCourses') as FormArray) repeated in template
  get additionalCourses(): FormArray {
    return this.enrollForm.get('additionalCourses') as FormArray;
  }

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.enrollForm = this.fb.group({
      studentName:       ['', [Validators.required, Validators.minLength(3)]],
      // Async validator as third argument — only fires after sync validators pass
      studentEmail:      ['', [Validators.required, Validators.email], [simulateEmailCheck]],
      courseId:          ['', [Validators.required, noCourseCode]],
      preferredSemester: ['Odd', Validators.required],
      agreeToTerms:      [false, Validators.requiredTrue],
      additionalCourses: this.fb.array([])
    });
  }

  addCourse(): void {
    this.additionalCourses.push(new FormControl('', Validators.required));
  }

  removeCourse(index: number): void {
    this.additionalCourses.removeAt(index);
  }

  onSubmit(): void {
    // enrollForm.value excludes disabled controls
    // enrollForm.getRawValue() includes ALL controls regardless of disabled state
    console.log('Form value:', this.enrollForm.value);
    console.log('Raw value:', this.enrollForm.getRawValue());
  }
}
