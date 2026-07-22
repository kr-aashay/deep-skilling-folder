import { createReducer, on } from '@ngrx/store';
import { enrollInCourse, unenrollFromCourse, setEnrolledCourses } from './enrollment.actions';

export interface EnrollmentState {
  enrolledCourseIds: number[];
}

const initialState: EnrollmentState = {
  enrolledCourseIds: [1, 3]
};

export const enrollmentReducer = createReducer(
  initialState,
  on(enrollInCourse,     (state, { courseId }) => ({
    ...state,
    enrolledCourseIds: state.enrolledCourseIds.includes(courseId)
      ? state.enrolledCourseIds
      : [...state.enrolledCourseIds, courseId]
  })),
  on(unenrollFromCourse, (state, { courseId }) => ({
    ...state,
    enrolledCourseIds: state.enrolledCourseIds.filter(id => id !== courseId)
  })),
  on(setEnrolledCourses, (state, { courseIds }) => ({
    ...state,
    enrolledCourseIds: courseIds
  }))
);
