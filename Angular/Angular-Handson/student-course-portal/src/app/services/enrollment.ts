import { Injectable } from '@angular/core';
import { Course } from '../models/course.model';
import { CourseService } from './course';

// Singleton enrollment service — injecting one service into another (service-to-service DI)
@Injectable({ providedIn: 'root' })
export class EnrollmentService {
  private enrolledCourseIds: number[] = [1, 3];

  // Inject CourseService to resolve course IDs to full Course objects
  constructor(private courseService: CourseService) {}

  enroll(courseId: number): void {
    if (!this.isEnrolled(courseId)) {
      this.enrolledCourseIds.push(courseId);
    }
  }

  unenroll(courseId: number): void {
    this.enrolledCourseIds = this.enrolledCourseIds.filter(id => id !== courseId);
  }

  isEnrolled(courseId: number): boolean {
    return this.enrolledCourseIds.includes(courseId);
  }

  getEnrolledIds(): number[] {
    return this.enrolledCourseIds;
  }

  getEnrolledCourses(): Course[] {
    return this.enrolledCourseIds
      .map(id => this.courseService.getCourseByIdLocal(id))
      .filter((c): c is Course => c !== undefined);
  }
}
