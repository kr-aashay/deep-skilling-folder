import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { CourseList } from './pages/course-list/course-list';
import { CourseDetail } from './pages/course-detail/course-detail';
import { StudentProfile } from './pages/student-profile/student-profile';
import { EnrollmentForm } from './pages/enrollment-form/enrollment-form';
import { ReactiveEnrollmentForm } from './pages/reactive-enrollment-form/reactive-enrollment-form';
import { NotFound } from './pages/not-found/not-found';
import { AuthGuard } from './guards/auth-guard';
import { UnsavedChangesGuard } from './guards/unsaved-changes-guard';

const routes: Routes = [
  // Home
  { path: '', component: Home },

  // Courses with nested routes — CourseList and CourseDetail share /courses prefix
  { path: 'courses',     component: CourseList },
  { path: 'courses/:id', component: CourseDetail },

  // Profile — protected by AuthGuard (Hands-On 7)
  { path: 'profile', component: StudentProfile, canActivate: [AuthGuard] },

  // Template-driven enrollment form
  { path: 'enroll', component: EnrollmentForm, canActivate: [AuthGuard] },

  // Reactive form — CanDeactivate guard warns on unsaved changes
  {
    path: 'enroll-reactive',
    component: ReactiveEnrollmentForm,
    canActivate: [AuthGuard],
    canDeactivate: [UnsavedChangesGuard]
  },

  // Wildcard — must be LAST; catches all unknown routes
  { path: '**', component: NotFound }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
