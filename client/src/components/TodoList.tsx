
import { MoonIcon, SunIcon } from '@chakra-ui/icons';
import { Button, IconButton, Input, Text, useColorMode } from '@chakra-ui/react';
import styled from '@emotion/styled';
import { useState } from 'react';
import { Todo } from './Todo';
import { useTodos } from '../services/useTodos';
import { useCreateTodo } from '../services/useCreateTodo';
import { useUpdateTodo } from '../services/useUpdateTodo';
import { useDeleteTodo } from '../services/useDeleteTodo';
import { GoogleLogin } from '@react-oauth/google';

const FlexRowWrapper = styled.div`
   display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  gap: 16px;
  padding: 16px 0;
`;

const FlexColumnWrapper = styled.div`
  display: flex;
  flex-direction: column;
`;

const Wrapper = styled.div`
  padding: 16px;
  max-width: 550px;
  margin: 40px auto;
`;

export const TodoList = () => {
    const { colorMode, toggleColorMode } = useColorMode();
    const [ todoName, setTodoName ] = useState('');

    const { data: todos } = useTodos();
    const createTodo = useCreateTodo();
    const updateTodo = useUpdateTodo();
    const deleteTodo = useDeleteTodo();

    return <Wrapper>
        <GoogleLogin
            useOneTap={true}
            onSuccess={(credentialResponse) => {
                sessionStorage.setItem('token', credentialResponse.credential!);
            }}
        />
        <FlexRowWrapper>
            <Text fontSize='5xl' as='b'>My tasks</Text>
            <IconButton 
                aria-label="Toggle color mode" 
                onClick={toggleColorMode} 
                size="lg"
                icon={colorMode === 'dark' ? <SunIcon></SunIcon> : <MoonIcon></MoonIcon>}
            >
            </IconButton>
        </FlexRowWrapper>
        <FlexColumnWrapper>
            {todos.map((todo: any) => <Todo
                key={todo.id}
                name={todo.name}
                isChecked={todo.checked}
                onChange={checked => updateTodo.mutate({ ...todo, checked })}
                onDelete={() => deleteTodo.mutate(todo.id)}
            />)}
        </FlexColumnWrapper>
        <FlexRowWrapper>
            <Input  
                placeholder='Type something to do...'  
                value={todoName} 
                size={'lg'}
                onChange={e => setTodoName(e.target.value)} 
            />
            <Button   
                colorScheme='teal' 
                size='lg' 
                onClick={() => {
                    createTodo.mutate(todoName);
                    setTodoName('');
                }} 
                isDisabled={todoName === ''} 
                isLoading={createTodo.isPending}
            >
                    Create
            </Button>
        </FlexRowWrapper>
    </Wrapper>;
};
