import * as React from 'react';
import 'src/assets/css/App.css'
import Movies from './Movies'

class App extends React.Component<any, IState> {

  public state: IState = {
    "movies": []
};
constructor(props: IState) {
  super(props);

} 

public async componentDidMount() {
  const result = await fetch('https://localhost:44371/v1/GetMovies');
  const movies = await result.json();
  this.setState({ movies });
   }
  public render() {
    return (
      <div className="App">
        <div className="App-header">
          <h1 className="App-title">Kino Studyjne</h1>
          </div>
        <p className="App-intro">
          Witaj w najlepszym kinie do jakiego mogłeś zajrzeć!<br/><br/>
          dzisiaj gramy:
        </p>
     <div className="App-intro"> 
          <img src="https://vignette.wikia.nocookie.net/garfield/images/b/ba/Garfield-profile.png/revision/latest?cb=20130807215550&path-prefix=pl"/>
          {this.state.movies.map(movie => 
                        <Movies key={movie.id} movie={movie}/>)}
      </div>
      </div>
    );
  }
}

export default App;
export interface IState {
  movies: IMovies[];
}

export interface IMovies {
  id: number,
  title: string,
  description: string
}