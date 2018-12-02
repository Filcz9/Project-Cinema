import * as React from "react";
import 'src/assets/css/App.css'
import 'src/assets/css/ReserveTicket.css';




class Seats extends React.Component<any, IState>{

    public state: IState={
        "free": true,
        
        "reservation":{
            "IdReservation": 1,
            "IdUserAccount": 1,
            "IdScreening": 3
        },
        
        "seat":{
            "idSeat": this.props.seat.idSeat,
            "rowNumb": this.props.seat.rowNumb,
            "seatNumb": this.props.seat.seatNumb
        },
        "seatReservation":
        {
            "idSeatReservation":10,
            "idSeat":8,
            "idReservation":1
        }

        };

    constructor(props: IState) {
            super(props);
          }
    public isFree = () => {

        this.setState({
          free: !this.state.free        
        })
        if(this.state.free)
        {this.SendSeat();}
        else 
        {
            this.RemoveSeat();
        }
      }
      public async SendSeat(){

    
        const result = await fetch('https://localhost:44371/cinema/AddSeat?reservation=' + this.state.reservation.IdReservation + '&seat=' + this.state.seat.idSeat, {
          method: 'POST'
        });
        await result.json();
    }
    public async RemoveSeat(){

    
        const result = await fetch('https://localhost:44371/cinema/RemoveSeat?reservation=' + this.state.reservation.IdReservation + '&seat=' + this.state.seat.idSeat, {
          method: 'GET'
        });
        await result.json();
    }


public render() {
    // if(this.props.seat.seatNumb !== 0){this.state.map = "seat-taken"  }
    // else { this.state.map = "seat-free" }
    let seatState = this.state.free ? 'seat-free' : 'seat-taken';
    if (this.state.seat.seatNumb === 0) {
        seatState = 'seat-empty';
      }
    if(this.props.seat.rowNumb === "Q")
    {
        
        return(
            <br/>           
        )
    }
    else{
        return (
            <div className= "line"> 
            <button className={seatState} onClick={this.isFree}>
            <p className="white">{this.props.seat.seatNumb}</p>
            </button>
            </div>
           
             
           )
        }
}


}
export default Seats;
export interface IState {
    free: boolean,
    
    seat: ISeat,
    reservation: IReservation,
    seatReservation: ISeatReservation
  }
  export interface IReservation {
    IdReservation: number,
    IdUserAccount: number,
    IdScreening: number,
  }
  export interface ISeat {
    idSeat: number,
    rowNumb: string,
    seatNumb: number,
    
  }
  export interface ISeatReservation {
    idSeatReservation: number,
    idSeat:number,
    idReservation: number,
    
  }