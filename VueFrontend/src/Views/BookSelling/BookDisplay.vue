<template> 
    <div class="container-fluid">
        <div class="container " align="center">
            <div class="badge badge-pill badge-dark  mt-4">
                <h1 align="center" class="display-4 mx-auto my-auto px-3 py-2">
                    All Books
                </h1>
            </div>
        </div>
        <!-- Need to tie to .net backend
            <div class ="container mt-3" align="center">
                <div class = "card p-3">
                    <h5 class = "card-title">Search Filters</h5>
                       <form method = "get">
                           <div class="form=row">
                               <label > Username: </label>
                                **Add form user
                               <label > Title: </label>
                                **Add form book name
                                <label > Publish Date: </label>
                                **add form publish date
                                <br>
                               <label > Min Price: </label>
                                **add form price range

                                 <label > Max Price: </label>
                                **Add form max price range
                                <button class ="btn btn-dark"> Search </button>
                           </div>
                        </form>
                    </div>
                </div>

            <table class="table table-hover mx-auto my-4" id="my-books-table" align="center" border="2" width="800">
                <thead class="thead-dark">
                <tr>
                    <th scope="col"> Name</th>
                    <th scope="col"> Price</th>
                    <th scope="col"> Date</th>
                    <th scope="col"> Image</th>
                    <th scope="col">Posted By</th>
                    <th scope="col">Review</th>

                </tr>
                </thead>
                {% for book in books %}
                    <tr>
                        <td class="lead">
                            <a href="book_detail/{{ book.id }}" align="">
                                {{ book.name }}
                            </a>
                        </td>
                        <td class="lead">{{ book.price }}</td>
                        <td class="lead">{{ book.publishdate }}</td>

                        <td class="lead"><img src="{% static book.pic_path %}" width="40" height="40" alt=""></td>
                        <td class="lead">
                            {{ book.username }}
                        </td>
                        <td class="lead">
                            <a href="leave_review/{{ book.id }}" align=""> Leave a review</a>
                        </td>
                    </tr>
                {% endfor %}
            </table>

            <div  class = "container" align="center">
                <div class ="row">

                {%if books.has_previous %}
                    <a class ="btn btn-inverse" href="{% replace_url 1 'page' request.GET.urlencode%}"> First</a>
                    <a class ="btn btn-inverse" href="{%replace_url books.previous_page_number 'page' request.GET.urlencode%}">  </a>
                {% endif %}

                <button class ="pgn-btn btn btn-dark mx-auto">{{books.number}}</button>

                {% if books.has_next %}
                    <a class ="btn btn-inverse" href="{%replace_url books.next_page_number 'page' request.GET.urlencode%}"> > </a>
                    <a class ="btn btn-inverse" href="{%replace_url books.paginator.num_pages 'page' request.GET.urlencode%}">Last</a>
                {% endif %}

                </div>
            </div>
            -->
        </div>
    </template>
