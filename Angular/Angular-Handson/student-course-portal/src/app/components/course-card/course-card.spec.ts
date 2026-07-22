import { ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { SimpleChange } from '@angular/core';
import { CourseCard } from './course-card';
import { CreditLabelPipe } from '../../pipes/credit-label-pipe';
import { Highlight } from '../../directives/highlight';
import { EnrollmentService } from '../../services/enrollment';
import { CourseService } from '../../services/course';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Course } from '../../models/course.model';

const mockCourse: Course = {
  id: 1, name: 'Data Structures', code: 'CS101', credits: 4, gradeStatus: 'passed', enrolled: false
};

describe('CourseCard', () => {
  let component: CourseCard;
  let fixture: ComponentFixture<CourseCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CourseCard, CreditLabelPipe, Highlight],
      imports: [HttpClientTestingModule],
      providers: [EnrollmentService, CourseService]
    }).compileComponents();

    fixture = TestBed.createComponent(CourseCard);
    component = fixture.componentInstance;
    component.course = mockCourse;
    fixture.detectChanges();
  });

  // Test 1: Component is created
  it('should create', () => {
    expect(component).toBeTruthy();
  });

  // Test 2: @Input — renders course name in h3
  it('should display the course name', () => {
    const h3 = fixture.debugElement.query(By.css('h3')).nativeElement;
    expect(h3.textContent).toContain('Data Structures');
  });

  // Test 3: @Input — renders course code
  it('should display the course code', () => {
    const code = fixture.debugElement.query(By.css('.course-code')).nativeElement;
    expect(code.textContent).toContain('CS101');
  });

  // Test 4: @Output — emit enrollRequested when Enroll button clicked
  it('should emit enrollRequested when Enroll button is clicked', () => {
    spyOn(component.enrollRequested, 'emit');
    const btn = fixture.debugElement.queryAll(By.css('button'))
      .find(b => b.nativeElement.textContent.trim().includes('Enroll'));
    btn?.nativeElement.click();
    fixture.detectChanges();
    expect(component.enrollRequested.emit).toHaveBeenCalledWith(1);
  });

  // Test 5: ngOnChanges logs previous and current value
  it('should log changes in ngOnChanges', () => {
    spyOn(console, 'log');
    const newCourse: Course = { ...mockCourse, name: 'Algorithms' };
    component.ngOnChanges({
      course: new SimpleChange(mockCourse, newCourse, false)
    });
    expect(console.log).toHaveBeenCalled();
  });
});
