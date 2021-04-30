import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AccountSettingsComponent} from './account-settings/account-settings-component'

const routes: Routes = [
  {
    path: 'account-settings',
   component : AccountSettingsComponent
   
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
