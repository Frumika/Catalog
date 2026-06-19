import type {SearchBarProps} from "@/widgets/header/ui/search-bar/SearchBar.types.ts";
import type {KeyboardEvent} from 'react';
import {Input} from '@/shared/ui/input';
import {Button} from '@/shared/ui/button';
import {Icon} from "@/shared/ui/icon";
import styles from "./SearchBar.module.css";
import SearchIcon from "@/shared/assets/search.svg?react";
import CrossIcon from "@/shared/assets/cross.svg?react";


export const SearchBar = (
    {
        value = '',
        onChange,
        onSearch,
        onClear,
        placeholder,
        className,
    }: SearchBarProps) => {

    const handleKeyDown = (event: KeyboardEvent<HTMLInputElement>) => {
        if (event.key === 'Enter') {
            onSearch?.(value);
        }
    }

    const searchBarStyles = [
        styles.searchBar,
        className,
    ].filter(Boolean).join(' ');

    return (
        <span className={searchBarStyles}>
            <span className={styles.wrapper}>
                <Input
                    className={styles.input}
                    value={value}
                    placeholder={placeholder}
                    onChange={onChange}
                    onKeyDown={handleKeyDown}
                />

                <Button
                    className={styles.deleteButton}
                    variant="ghost"
                    size="small"
                    icon={<Icon><CrossIcon/></Icon>}
                    onClick={() => onClear?.()}
                />
            </span>

            <Button
                className={styles.searchButton}
                variant="primary"
                size="medium"
                icon={<Icon><SearchIcon/></Icon>}
                onClick={() => onSearch?.(value)}
            />
        </span>
    );
};