<template>
    <div class="container-fluid">
        <div class="container " align="center">
            <div class="badge badge-pill badge-dark  mt-4">
                <h1 align="center"
                    class="display-4 mx-auto my-auto px-3 py-2">
                    Search
                    Results
                </h1>
            </div>
        </div>
        <!-- Need to tie to .net backend
            <table class="table table-hover mx-auto my-4" id="my-books-table" align="center" border="2" width="800">
                <thead class="thead-dark">
                <tr>
                    <th scope="col"> Name</th>
                    <th scope="col"> Price</th>
                    <th scope="col"> Date</th>
                    <th scope="col"> Image</th>
                    <th scope="col">Posted By</th>
                </tr>
                </thead>
                {% for book in results %}
                    <tr>
                        <td class="lead">
                            <a href="book_detail/{{ book.id }}">
                                {{ book.name }}
                            </a>
                        </td>
                        <td class="lead">{{ book.price }}</td>
                        <td class="lead">{{ book.publishdate }}</td>

                        <td class="lead"><img src="{% static book.pic_path %}" width="40" height="40" alt=""></td>
                        <td class="lead">
                            {{ book.username }}
                        </td>

                    </tr>
                {% endfor %}     
            </table>
        -->
        </div>
    </template>
