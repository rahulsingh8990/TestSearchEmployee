import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { EmployeeDetails } from 'src/Model/EmployeeDetails';
import { SearchCriteria } from 'src/Model/SearchCriteria';
import { EmployeeServiceService } from '../employee-service.service';

@Component({
  selector: 'app-employee-search',
  templateUrl: './employee-search.component.html',
  styleUrls: ['./employee-search.component.css']
})
export class EmployeeSearchComponent implements OnInit {

  userForm = new FormGroup({
    name: new FormControl('',[]),
    startDate: new FormControl('',[]),
    endDate: new FormControl('',[])
  })
  employeeDetailsList:EmployeeDetails[];
  errorText:string;
  showError:boolean;
  startDate: Date;
  endDate: Date;
  constructor(private employeeService:EmployeeServiceService) { }

  ngOnInit() {
  }
  
  onSubmit(){
    
    if(this.userForm.get('name').value==''&& this.userForm.get('startDate').value=='' && this.userForm.get('endDate').value=='' ){
        this.errorText ='Please select either Name or DateRange';
        this.showError = true;
        return;
    }
    if(this.userForm.get('startDate').value!=''){
      if(this.userForm.get('endDate').value=='')
      {
        this.errorText ='Please select End date';
        this.showError = true;
        return;
      }
      
    }
    if(this.userForm.get('endDate').value!=''){
      if(this.userForm.get('startDate').value=='')
      {
        this.errorText ='Please select start date';
        this.showError = true;
        return;
      }
      if(!this.checkDate(this.userForm.get('startDate').value, this.userForm.get('endDate').value)){
        this.errorText ='Please Enter Valid Date';
          this.showError = true;
          return;
      }
    }
   
    this.showError=false;

   let model =<SearchCriteria>{
     Criteria:'',
     Name:this.userForm.get('name').value,
     StartDate:this.userForm.get('startDate').value,
     EndDate:this.userForm.get('endDate').value
   }
   this.employeeService.GetEmployee(model).subscribe(res=>{
     this.employeeDetailsList = res
   })
  }

  checkDate(startDate:string,endDate:string){
    let stDate:string[] = startDate.split('/');
    let edDate:string[] = endDate.split('/');
    if(stDate && edDate){
        let newDate : Date = new Date(Number(stDate[2]),Number(stDate[1])-1,Number(stDate[0]));
        let endDate : Date = new Date(Number(edDate[2]),Number(edDate[1])-1,Number(edDate[0]));
        if(newDate.getDate() && endDate.getDate()){
          if(newDate<endDate)
          {
            return false;
          }
          this.startDate = newDate;
          this.endDate = endDate;
          return true;
        }
    }
    return false;
  }
   

}
