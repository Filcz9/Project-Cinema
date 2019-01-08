import * as React from "react";
import './Navbar.css'

import Fade from '../App/Fade';
import { NavLink } from 'react-router-dom';
import Registration from '../Users/Registration';
import Login from '../Users/Login';
import Newsletter from '../Newsletter/Newsletter';
import UserOptions from '../Users/UserOptions';
import Logout from '../Users/Logout';
import decode from 'jwt-decode';

class Navbar extends React.Component<any, IState>{

  public state: IState = {
    "user": []
  };

  constructor(props: IState) {
    super(props);
  }

  public componentDidMount(){
    try{
    const userStorage = localStorage.getItem("User");
      if (userStorage !== null){
        const user = JSON.parse(userStorage);
        console.log(userStorage);
        const { exp } = decode(user.response.token);
        console.log(exp);
        if (exp < new Date().getTime() / 1000){
          this.refreshToken(user);
        }
      }
    } catch (e){
      return false;
    }
    return true;
}

public async refreshToken(user: any){
  await fetch('https://localhost:44371/api/refresh', {
    method: 'post',
    headers: {
      'Accept': 'application/json, text/plain, */*',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({token: user.response.token, refreshToken: user.response.refreshToken})
  }).then(res2=>res2.json())
     .then(res2 => console.log(res2))
}


  public render() {
    function CheckUser(){
      let isUserLogged = false;
      
      const userStorage = localStorage.getItem("User");
    
      
    
     
      if (userStorage != null){
        isUserLogged = true;
      }
        if (isUserLogged) {
          return     <><Logout/><UserOptions/></>;
        }
        else{
          return   <><Login/><Registration/></>;  
         
        }
    }
 

    return (

      <nav className="flex items-center justify-between flex-wrap .bg-black p-4">
       
        <div className="w-full block flex-grow lg:flex lg:items-center lg:w-auto">
          <Fade>
          <div className="monte text-white text-center text-xl lg:flex-grow">
            <NavLink to="/" className="monte-bold text-white no-underline text-3xl tracking-tight text-center mr-20">Kino Studyjne</NavLink>
            <NavLink to="/Repertuar" className="block no-underline mt-4 lg:inline-block lg:mt-0 text-white mr-6">Repertuar</NavLink>
            <NavLink to="/Events" className="block no-underline mt-4 lg:inline-block lg:mt-0 text-white mr-6">Wydarzenia</NavLink>
            <NavLink to="/AdminPanel" className="block mt-4 no-underline lg:inline-block lg:mt-0 text-white mr-6">Administracja</NavLink>
            <CheckUser/>
            <Newsletter/>
     
          </div>
          </Fade>
        </div>
      </nav>

    )
  }

}

export default Navbar;

export interface IState {
  user: IUser[];
}

export interface IUser {
  id: number,
  username: string
}