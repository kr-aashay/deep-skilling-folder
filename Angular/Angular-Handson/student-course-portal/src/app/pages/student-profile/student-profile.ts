import { Component, OnInit } from '@angular/core';
import { Course } from '../../models/course.model';
import { EnrollmentService } from '../../services/enrollment';

@Component({
  selector: 'app-student-profile',
  standalone: false,
  templateUrl: './student-profile.html',
  styleUrl: './student-profile.css'
})
export class StudentProfile implements OnInit {
  enrolledCourses: Course[] = [];

  // Hands-On 6: Inject EnrollmentService to display enrolled courses
  constructor(private enrollmentService: EnrollmentService) {}

  ngOnInit(): void {
    this.enrolledCourses = this.enrollmentService.getEnrolledCourses();
  }
}
