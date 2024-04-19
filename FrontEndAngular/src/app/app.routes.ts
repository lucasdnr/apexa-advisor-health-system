import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { NewAdvisorComponent } from './pages/advisor/new-advisor/new-advisor.component';
import { EditAdvisorComponent } from './pages/advisor/edit-advisor/edit-advisor.component';

export const routes: Routes = [
    {
        path:'', component: HomeComponent
    },
    {
        path: 'advisor', component: NewAdvisorComponent
    },
    {
        path:'advisor/:id', component: EditAdvisorComponent
    }
];
