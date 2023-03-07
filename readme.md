This project demostrates how to pull a wordpress API, do some basic processing (in this case, a word count), and serve the result on as a webpage.

### How to run

There is a file called `setup.sh`, which you can simply run. It will build everything from scratch, and will start a docker container serving the webpage. It should be avaliable on `http://localhost:1000`. On my windows machine, it worked properly only under the wsl (Window Subsystem for Linux).

### Notes about the webpage in general:

In the navbar there are 3 links, which are 3 different implementations for this task. They're ordered from simple to less simple.

* Index - Just pulls the metadata from the Wordpress-API, displays some articles with links to the articles.
* React - It's a react app, which will pull the metadata, and also preform the word-count. You can see the result if you click on "".
* React - Is the same as the previous one, but it communicates via a websocket, and will recieve updates every few seconds. It should update, if the article on the remote website is changed. You have to press "Get word-count with a socket", to start the connection. You can choose which article you want to get updates for and which not.

### Notes about the code:

This is only a proof of concept level, and not a production level that you can send a costumer. There are several things that I didn't solve well, and there is an issue with copy-paste code. That is only there to keep the first two options functioning. Eg. TheKeyAcademyController.cs:63 - in the websocket version, there is no need for theMap to be sent, as this is not used, and the data is later obtained via the websocket.