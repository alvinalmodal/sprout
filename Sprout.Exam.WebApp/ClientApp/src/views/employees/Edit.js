import React, { Component } from 'react';
import authService from '../../components/api-authorization/AuthorizeService';
import { displayFieldError, displayUnhandledErrors } from '../../common/DisplayError';

export class EmployeeEdit extends Component {
  static displayName = EmployeeEdit.name;

  constructor(props) {
    super(props);
    this.state = { id: 0,fullName: '',birthdate: '',tin: '',typeId: 1, loading: true,loadingSave:false, errors:[] };
  }

  componentDidMount() {
    this.getEmployee(this.props.match.params.id);
  }
  handleChange(event) {
    this.setState({ [event.target.name] : event.target.value});
  }

  handleSubmit(e){
      e.preventDefault();
      if (window.confirm("Are you sure you want to save?")) {
        this.saveEmployee();
      } 
    }

    convertDateTime(date) {
        if (date instanceof Date) {
            const year = date.getUTCFullYear();
            const month = `${date.getUTCMonth() + 1}`.padStart(2, "0");
            const day = `${date.getUTCDate()}`.padStart(2, "0");
            return `${year}-${month}-${day}`;
        }
        return date;
    }

  render() {

    let contents = this.state.loading
    ? <p><em>Loading...</em></p>
    : <div>
    <form>
<div className='form-row'>
<div className='form-group col-md-6'>
  <label htmlFor='inputFullName4'>Full Name: *</label>
    <input type='text' className='form-control' id='inputFullName4' onChange={this.handleChange.bind(this)} name="fullName" value={this.state.fullName} placeholder='Full Name' />
    {displayFieldError(this.state.errors, "fullName")}
</div>
<div className='form-group col-md-6'>
     <label htmlFor='birthdate'>Birthdate: *</label>
    <input type='date' className='form-control' id='birthdate' onChange={this.handleChange.bind(this)} name="birthdate" value={this.convertDateTime(this.state.birthdate)} placeholder='Birthdate' />
    {displayFieldError(this.state.errors, "birthdate")}
</div>
</div>
<div className="form-row">
<div className='form-group col-md-6'>
  <label htmlFor='inputTin4'>TIN: *</label>
    <input type='text' className='form-control' id='inputTin4' onChange={this.handleChange.bind(this)} value={this.state.tin} name="tin" placeholder='TIN' />
    {displayFieldError(this.state.errors, "tin")}
</div>
<div className='form-group col-md-6'>
  <label htmlFor='inputEmployeeType4'>Employee Type: *</label>
  <select id='inputEmployeeType4' onChange={this.handleChange.bind(this)} value={this.state.typeId}  name="typeId" className='form-control'>
    <option value='1'>Regular</option>
    <option value='2'>Contractual</option>
    </select>
    {!this.state.typeId && displayFieldError(this.state.errors, "typeId")}
</div>
</div>
<button type="submit" onClick={this.handleSubmit.bind(this)} disabled={this.state.loadingSave} className="btn btn-primary mr-2">{this.state.loadingSave?"Loading...": "Save"}</button>
<button type="button" onClick={() => this.props.history.push("/employees/index")} className="btn btn-primary">Back</button>
</form>
{displayUnhandledErrors(this.state.errors, this.state)}
</div>;


    return (
        <div>
        <h1 id="tabelLabel" >Employee Edit</h1>
        <p>All fields are required</p>
        {contents}
      </div>
    );
  }

  async saveEmployee() {
    this.setState({ loadingSave: true, errors : [] });
    const { id, fullName, birthdate, tin, typeId } = this.state;
    const token = await authService.getAccessToken();
    const requestOptions = {
        method: 'PUT',
        headers: !token ? {} : { 'Authorization': `Bearer ${token}`,'Content-Type': 'application/json' },
        body: JSON.stringify({
            id,
            fullName,
            birthdate,
            tin,
            typeId
        })
    };
    const response = await fetch('api/employees/' + this.state.id,requestOptions);

    if(response.status === 200){
        this.setState({ loadingSave: false });
        alert("Employee successfully saved");
        this.props.history.push("/employees/index");
    }
    else{
        var result = await response.json();
        this.setState({ errors: result["errors"], loadingSave: false });
    }
  }

  async getEmployee(id) {
    this.setState({ loading: true,loadingSave: false });
    const token = await authService.getAccessToken();
    const response = await fetch('api/employees/' + id, {
      headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    });
      const data = await response.json();
      this.setState({ id: data.id, fullName: data.fullName, birthdate: new Date(data.birthdate.replace('T00:00:00','')), tin: data.tin, typeId: data.typeId, loading: false, loadingSave: false });
  }
}
