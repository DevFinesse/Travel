import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';
import { DashboardComponent } from './features/admin/dashboard/dashboard.component';
import { TourListComponent } from './features/admin/tour-list/tour-list.component';
import { TourFormComponent } from './features/admin/tour-form/tour-form.component';
import { BookingListComponent } from './features/admin/booking-list/booking-list.component';
import { HomeComponent } from './features/home/home.component';
import { TourCatalogComponent } from './features/tours/tour-catalog/tour-catalog.component';
import { TourDetailsComponent } from './features/tours/tour-details/tour-details.component';
import { UserBookingsComponent } from './features/booking/user-bookings/user-bookings.component';
import { authGuard } from './core/guards/auth.guard';
import { adminGuard } from './core/guards/admin.guard';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'tours', component: TourCatalogComponent },
    { path: 'tours/:id', component: TourDetailsComponent },
    {
        path: 'my-bookings',
        component: UserBookingsComponent,
        canActivate: [authGuard]
    },
    {
        path: 'admin',
        canActivate: [adminGuard],
        children: [
            { path: '', component: DashboardComponent },
            { path: 'tours', component: TourListComponent },
            { path: 'tours/new', component: TourFormComponent },
            { path: 'tours/edit/:id', component: TourFormComponent },
            { path: 'bookings', component: BookingListComponent }
        ]
    },
    { path: '**', redirectTo: '' }
];
