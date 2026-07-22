import { ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { MemoizedSelector } from '@ngrx/store';
import { MockStore, provideMockStore } from '@ngrx/store/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';
import { CourseList } from './course-list';
import { CourseCard } from '../../components/course-card/course-card';
import { CreditLabelPipe } from '../../pipes/credit-label-pipe';
import { Highlight } from '../../directives/highlight';
import { EnrollmentService } from '../../services/enrollment';
import { CourseService } from '../../services/course';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Course } from '../../models/course.model';
import { selectAllCourses, selectCoursesLoading, selectCoursesError } from '../../store/course/course.selectors';

const mockCourses: Course[] = [
  { id: 1, name: 'Data Structures', code: 'CS101', credits: 4, gradeStatus: 'passed' },
  { id: 2, name: 'Algorithms',       code: 'CS102', credits: 4, gradeStatus: 'pending' }
];

describe('CourseList', () => {
  let component: CourseList;
  let fixture: ComponentFixture<CourseList>;
  let store: MockStore;
  let mockSelectAllCourses: MemoizedSelector<object, Course[]>;
  let mockSelectLoading: MemoizedSelector<object, boolean>;
  let mockSelectError: MemoizedSelector<object, string | null>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CourseList, CourseCard, CreditLabelPipe, Highlight],
      imports: [HttpClientTestingModule, RouterTestingModule, FormsModule],
      providers: [
        provideMockStore({
          initialState: {
            course:     { courses: mockCourses, loading: false, error: null },
            enrollment: { enrolledCourseIds: [] }
          }
        }),
        EnrollmentService,
        CourseService
      ]
    }).compileComponents();

    store    = TestBed.inject(MockStore);
    fixture  = TestBed.createComponent(CourseList);
    component = fixture.componentInstance;

    mockSelectAllCourses = store.overrideSelector(selectAllCourses, mockCourses);
    mockSelectLoading    = store.overrideSelector(selectCoursesLoading, false);
    mockSelectError      = store.overrideSelector(selectCoursesError, null);

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  // Test: Renders course cards when courses are in the store
  it('should render course cards from store', () => {
    const cards = fixture.debugElement.queryAll(By.css('app-course-card'));
    expect(cards.length).toBe(2);
  });

  // Test: Loading indicator shown when loading is true
  it('should show loading indicator when loading is true', () => {
    mockSelectLoading.setResult(true);
    store.refreshState();
    fixture.detectChanges();

    const loading = fixture.debugElement.query(By.css('.loading'));
    expect(loading).toBeTruthy();
  });

  // Test: No courses template shown when courses array is empty
  it('should show no-courses message when list is empty', () => {
    mockSelectAllCourses.setResult([]);
    store.refreshState();
    fixture.detectChanges();

    const noCoursesEl = fixture.debugElement.query(By.css('.no-courses'));
    expect(noCoursesEl).toBeTruthy();
  });
});
