describe('GPA', function () {

    // User enters in all inforomation 
    it('Calculate GPA', function () {
        // Logs in 
        cy.visit('https://localhost:5002')

        cy.contains('Test').click()

        cy.contains('Test').click()

        cy.contains('GPA/Grade Calculator').click()

        cy.contains('Calculate GPA').click()

        // Making selectiovns

        cy.get('[v-model="one"]').select('A')

        cy.get('input[id="unit1"]').type('3')
        cy.get('input[id="unit2"]').type('3')
        cy.get('input[id="unit3"]').type('3')
        cy.get('input[id="unit4"]').type('3')

        // Saves
        cy.contains('Calculate GPA!').click()

        

    })

    // Missing grade selection choices 
    it('Missing Grade', function () {
        cy.visit('https://localhost:5002/gpaCalc')

        // Selections
        cy.get('input[id="unit1"]').type('3')
        cy.get('input[id="unit2"]').type('3')
        cy.get('input[id="unit3"]').type('3')
        cy.get('input[id="unit4"]').type('3')

        // Doesnt save, error message shown
        cy.contains('Calculate GPA!').click()
    })

    // Missing Units 
    it('Missing Units', function () {
        cy.visit('https://localhost:5002/gpaCalc')

        // Selections

        // Doesnt save, error message shown
        cy.contains('Calculate GPA!').click()

    })


})
