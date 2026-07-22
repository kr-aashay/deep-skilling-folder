import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';

import { AppRoutingModule } from './app-routing-module';

// Root component
import { App } from './app';

// Components
import { Header } from './components/header/header';
import { CourseCard } from './components/course-card/course-card';

// Pages
import { Home } from './pages/home/home';
import { CourseList } from './pages/course-list/course-list';
import { CourseDetail } from './pages/course-detail/course-detail';
import { StudentProfile } from './pages/student-profile/student-profile';
import { EnrollmentForm } from './pages/enrollment-form/enrollment-form';
import { ReactiveEnrollmentForm } from './pages/reactive-enrollment-form/reactive-enrollment-form';
import { NotFound } from './pages/not-found/not-found';

// Directives & Pipes
import { Highlight } from './directives/highlight';
import { CreditLabelPipe } from './pipes/credit-label-pipe';

// Interceptors
import { AuthInterceptor } from './interceptors/auth-interceptor';
import { ErrorHandlerInterceptor } from './interceptors/error-handler-interceptor';
import { LoadingInterceptor } from './interceptors/loading-interceptor';

// NgRx Store
import { courseReducer } from './store/course/course.reducer';
import { enrollmentReducer } from './store/enrollment/enrollment.reducer';
import { CourseEffects } from './store/course/course.effects';

@NgModule({
  declarations: [
    App,
    Header,
    CourseCard,
    Home,
    CourseList,
    CourseDetail,
    StudentProfile,
    EnrollmentForm,
    ReactiveEnrollmentForm,
    NotFound,
    Highlight,
    CreditLabelPipe
  ],
  imports: [
    BrowserModule,
    FormsModule,            // Hands-On 4: ngModel, template-driven forms
    ReactiveFormsModule,    // Hands-On 5: FormBuilder, FormGroup, FormArray
    HttpClientModule,       // Hands-On 8: HttpClient
    AppRoutingModule,

    // Hands-On 9: NgRx store
    StoreModule.forRoot({
      course:     courseReducer,
      enrollment: enrollmentReducer
    }),
    EffectsModule.forRoot([CourseEffects]),
    StoreDevtoolsModule.instrument({ maxAge: 25 })
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    // Hands-On 8: Register HTTP interceptors in order
    // Request goes through interceptors top→bottom; response bottom→top
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor,        multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor,     multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorHandlerInterceptor, multi: true }
  ],
  bootstrap: [App]
})
export class AppModule {}
