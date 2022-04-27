const domain = "localhost";
const apiPort = 5003;
let root = `http://${domain}/`;
let apiRoot = `http://${domain}:${apiPort}/api/`;
const URLS = {
    domain: domain,
    root: root,
    apiRoot: apiRoot,
    api: {
        recipeBuilder: {
            getRecipe: apiRoot + "recipe/getRecipe",
            newRecipe: apiRoot + "recipe/newRecipe",
            updateRecipe: apiRoot + "recipe/updateRecipe",
            deleteRecipe: apiRoot + "recipe/deleteRecipe"
        },
    }
}

export default URLS