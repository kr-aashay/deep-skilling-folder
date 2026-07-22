import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Course } from '../../models/course.model';
import { CourseService } from '../../services/course';

@Component({
  selector: 'app-course-detail',
  standalone: false,
  templateUrl: './course-detail.html',
  styleUrl: './course-detail.css'
})
export class CourseDetail implements OnInit {
  course: Course | undefined;
  errorMessage = '';

  constructor(
    private route: ActivatedRoute,
    private courseService: CourseService
  ) {}

  ngOnInit(): void {
    // Hands-On 7: Read route parameter :id
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.course = this.courseService.getCourseByIdLocal(id);
    if (!this.course) {
      this.errorMessage = `Course with ID ${id} not found.`;
    }
  }
}
