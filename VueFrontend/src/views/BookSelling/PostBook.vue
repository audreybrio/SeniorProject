<template>
    <div class="container">
        <div class="container " align="center">
            <div class="badge badge-pill badge-dark  mt-4">
                <h1 align="center"
                    class="display-4 mx-auto my-auto px-3 py-2">
                    Post Book
                </h1>
            </div>
        </div>
        <!-- Need to tie to .net backend objects
            <div class="container my-auto py-4">
                {% if submitted %}
                    <div class="alert alert-success" role="alert">
                        Post Successful!
                    </div>
                {% else %}
                    <div class="container ">
                        <form action="" enctype="multipart/form-data" method="post" novalidate>
                            <div class="form-group">
                                <label class="lead">Name: </label>
                                **Add form name
                            </div>
                            <div class="form-group">
                                <label class="lead">URL: </label>
                                **Add form url
                            </div>
                            <div class="form-group">
                                <label class="lead">Price: </label>
                                **Add form price
                            </div>
                            <div class="mt-4">
                            <div class="custom-file">
                                <label class="lead custom-file-label" for="image-upload">Select an image.. </label>
                                **Add form picture
                                {#                        <input type="file" class="form-control-file" id="image-upload">#}
                            </div>
                            </div>
                            {#                        {{ Add form table }}#}
                            <div class="container" align="right" >
                            <input class="btn btn-lg btn-secondary my-4" style="background-color: #008891; color: #FFFFFF;" type="submit" value="Submit">
                                </div>
                            {% csrf_token %}
                        </form>
                    </div>
                {% endif %}
            </div>
            -->
        </div>
    </template>
