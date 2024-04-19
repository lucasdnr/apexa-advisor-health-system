import { Routes } from '@angular/router';
import { AdvisorComponent } from './pages/advisor/advisor.component';
import { HomeComponent } from './pages/home/home.component';

export const routes: Routes = [
    {
        path: 'advisor', component: AdvisorComponent
    },
    {
        path:'', component: HomeComponent
    }
];
