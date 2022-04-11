<template>
<h1>Book Selling Home page</h1>
  <div class="bookSelling">
    <div class="container my-3">
                <p align="center">The book marketplace allows users to buy, sell, and browse books.
                    Posts will contain a checkmark if the seller is confirmed to have taken the course that uses the book.</p>
    </div>
  </div>
  <div class="vx-row">
            <div class="vx-col">
                <div class="card">
                    <img class="card-img-top" src="browse.jpg" height="400px" width="450px" alt="Card image cap">
                    <div class="card-body">
                        <h3 class="card-title">Browse</h3>
                        <p class="card-text">Search the collection of books.</p>
                        <router-link to="/bookDisplay" class="btn" style="background-color: #008891; color: #FFFFFF;">Browse Books</router-link>
                    </div>
                </div>
            </div>
            <div class="vx-col">
                <div class="card">
                    <img class="card-img-top" src="sell.jpg" height="400px" width="450px" alt="Card image cap">
                    <div class="card-body">
                        <h3 class="card-title">Sell</h3>
                        <p class="card-text">Book collection beginning to take up space? Rare book collector? Sell books
                            right from your collection at the price you find fair.</p>
                        <router-link to="/bookPost" class="btn btn-primary" style="background-color: #008891; color: #FFFFFF;">Sell Book</router-link>
                    </div>
                </div>
            </div>
        </div>
</template>